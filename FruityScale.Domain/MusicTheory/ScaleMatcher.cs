using FruityScale.Domain.Models;
using FruityScale.Domain.Services;

namespace FruityScale.Domain.MusicTheory;

public class ScaleMatcher : IScaleMatcher
{
    public IEnumerable<ScaleMatchResult> Match(IEnumerable<int> userNotes, IEnumerable<ScaleDefinition> library)
    {
        var uniqueNotes = userNotes.Select(n => n % MusicConstants.NotesInOctave).ToHashSet();
        
        var results = new List<ScaleMatchResult>();
        
        if (!uniqueNotes.Any()) return results;

        foreach (var scale in library)
        {
            for (int i = 0; i < MusicConstants.NotesInOctave; i++)
            {
                var scaleNotes = scale.Intervals
                    .Select(interval => (interval + i) % MusicConstants.NotesInOctave)
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