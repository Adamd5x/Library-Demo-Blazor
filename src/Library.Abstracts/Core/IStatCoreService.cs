using Library.Models.Dto;

namespace Library.Abstracts.Core;

public interface IStatCoreService
{
    Task<StatDto> GetStatAsync ();
}
