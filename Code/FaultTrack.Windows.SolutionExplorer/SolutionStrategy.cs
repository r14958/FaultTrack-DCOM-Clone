namespace FaultTrack.Windows.SolutionExplorer
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Linq;

    public class SolutionStrategy
    {
        private readonly string pattern;
        private readonly int groupIndex;
        private readonly int nameIndex;
        private readonly bool isBaseline;

        public SolutionStrategy(string pattern, int groupIndex)
        {
            this.pattern = pattern;
            this.groupIndex = groupIndex;
            this.isBaseline = true;
        }

        public SolutionStrategy(string pattern, int groupIndex, int nameIndex)
        {
            this.pattern = pattern;
            this.groupIndex = groupIndex;
            this.nameIndex = nameIndex;
        }
        
        public string Pattern => pattern;

        public string GetGroupName(FileInfo file)
        {
            return GetName(file, groupIndex);
        }

        public string GetName(FileInfo file)
        {
            if (isBaseline)
                return "Baseline";
            else
                return GetName(file, nameIndex);
        }

        private string GetName(FileInfo file, int index)
        {
            if (index == 0)
            {
                return file.Directory.Name;
            }

            var parts = file.FullName.Split('\\').Reverse().ToArray();

            return parts[index];
        }

        public bool IsMatch(FileInfo file)
        {
            return Regex.IsMatch(file.FullName, pattern);
        }
    }
}