using var inputReader = new StreamReader(Path.Join(Directory.GetCurrentDirectory(), "input.txt"));

int part1Answer = 0, part2Answer = 0;
while (!inputReader.EndOfStream)
{
    var line = await inputReader.ReadLineAsync();
    var reports = line!.Split(" ");
    if (IsSafe(reports))
    {
        part1Answer++;
    }
    else
    {
        for (int i = 0; i < reports.Length; i++)
        {
            if (IsSafe(reports, i))
            {
                part2Answer++;
                break;
            }
        }
    }
}
Console.WriteLine($"First part: {part1Answer}");
Console.WriteLine($"Second part: {part1Answer + part2Answer}");
return;

bool IsSafe(string[] reports, int skipIndex = -1)
{
    int left = 0, right = 1, corrNegDiff = 0, corrPosDiff = 0;
    while (right < reports.Length)
    {
        if (left == skipIndex)
        {
            left = right;
            right++;
        }

        if (right == skipIndex)
        {
            right++;
            if (right == reports.Length)
            {
                break;
            }
        }

        int report1 = int.Parse(reports[left]), report2 = int.Parse(reports[right]);
        var diff = report1 - report2;
        if (Math.Abs(diff) > 0 && Math.Abs(diff) <= 3)
        {
            if (diff < 0)
            {
                corrNegDiff++;
            }
            else
            {
                corrPosDiff++;
            }
        }
        left = right;
        right++;
    }
    if(skipIndex == -1)
    {
        if (corrPosDiff == reports.Length - 1 || corrNegDiff == reports.Length - 1)
        {
            return true;
        }
    }
    else
    {
        if (corrPosDiff == reports.Length - 2 || corrNegDiff == reports.Length - 2)
        {
            return true;
        }
    }
    return false;
}