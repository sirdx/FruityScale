using FruityScale.Domain.Models;

namespace FruityScale.Domain.Services;

public interface IScaleMatcher
{
    IEnumerable<ScaleMatchResult> Match(IEnumerable<int> userNotes, IEnumerable<ScaleDefinition> library);
}