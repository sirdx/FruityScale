using FruityScale.Domain.Models;

namespace FruityScale.Application.Contracts;

public interface INoteProvider
{
    Task<List<NoteEvent>> LoadNotesAsync(string source);
}