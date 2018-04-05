using System;
using System.Linq;
using System.Text.RegularExpressions;
using Google.Cloud.Vision.V1;

namespace ScreenshotAI.GoogleVision
{
    public class EggFeaturesRecognizionService
    {
        private const string CountdownRegex = @".*?(\d?\d:\d\d:\d\d).*";
        private const string TimeRegex = @".*?(\d?\d:\d\d)";

        public string ExtractGymName(TextAnnotation textAnnotation)
        {
            var page = textAnnotation.Pages.First();
            double minYRatio = 0.04583;
            double maxYRatio = 0.14;
            double minXRatio = 0.185;

            var gymNameBlock = page.Blocks.First(b =>
                b.BoundingBox.Vertices.First().Y / (double)page.Height > minYRatio &&
                b.BoundingBox.Vertices.Last().Y / (double)page.Height < maxYRatio &&
                b.BoundingBox.Vertices.First().X / (double)page.Width > minXRatio
            );

            string extractedGymName = string.Join("", gymNameBlock.Paragraphs.First().Words.SelectMany(w => w.Symbols.Aggregate(
                " ", (workingSentence, next) => workingSentence + next.Text.Trim()
            ))).Trim();

            return extractedGymName;
        }

        public TimeSpan ExtractCountDown(TextAnnotation textAnnotation)
        {
            var timeSpanString = ExtractPattern(textAnnotation, CountdownRegex);

            var timeSpanTokens = timeSpanString.Split(":");
            return new TimeSpan(
                int.Parse(timeSpanTokens[0]),
                int.Parse(timeSpanTokens[1]),
                int.Parse(timeSpanTokens[2])
            );
        }

        public TimeSpan ExtractTime(TextAnnotation textAnnotation)
        {
            var timeSpanString = ExtractPattern(textAnnotation, TimeRegex);

            var timeSpanTokens = timeSpanString.Split(":");
            return new TimeSpan(
                int.Parse(timeSpanTokens[0]),
                int.Parse(timeSpanTokens[1]),
                0
            );
        }

        private string ExtractPattern(TextAnnotation textAnnotation, string regex)
        {
            var page = textAnnotation.Pages.First();

            var allStrings = page.Blocks.SelectMany(
                b => b.Paragraphs.Select(
                    p => p.Words.Aggregate(
                        "", (all, next) => all + string.Join("", next.Symbols.Select(s => s.Text))
                    )));

            string matchedString = allStrings.First(s => Regex.IsMatch(s, regex));
            matchedString = Regex.Replace(matchedString, regex, "$1");

            return matchedString;
        }
    }
}
