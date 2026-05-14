using FruityScale.Domain.Models;
using FruityScale.Domain.Services;

namespace FruityScale.Domain.MusicTheory;

public class ScaleMatcher : IScaleMatcher
{
    public IEnumerable<ScaleMatchResult> Match(IEnumerable<int> userNotes, IEnumerable<ScaleDefinition> library)
    {
        var uniqueNotes = userNotes.Select(n => n % 12).ToHashSet();
        
        var results = new List<ScaleMatchResult>();
        
        if (!uniqueNotes.Any()) return results;

        foreach (var scale in library)
        {
            // Magic number alert:
            // octave consist of 12 notes so we iterate through every possible root note
            // TODO: It wouldn't be dumb to add this to project-wide constants or something because it's used in few places
            for (int i = 0; i < 12; i++)
            {
                var scaleNotes = scale.Intervals
                    .Select(interval => (interval + i) % 12)
                    .ToHashSet();
                
                var matches = uniqueNotes.Where(n => scaleNotes.Contains(n)).ToList();
                var wrongNotes = uniqueNotes.Where(n => !scaleNotes.Contains(n)).ToList();
                var missingNotes = scaleNotes.Where(n => !uniqueNotes.Contains(n)).ToList();
                
                double score = uniqueNotes.Count > 0 
                    ? (double)matches.Count / uniqueNotes.Count 
                    : 0;
                
                if (score > 0)
                {
                    results.Add(new ScaleMatchResult(
                        Scale: scale,
                        RootNote: i,
                        Score: score,
                        WrongNotes: wrongNotes,
                        MissingNotes: missingNotes
                    ));
                }
            }
        }
        
        return results.OrderByDescending(r => r.Score).ToList();
    }
}