using AppData.Entities;

namespace BlazorServer.IServices
{
	public interface IFieldShiftService
	{
		Task<List<FieldShift>> GetAllFieldshiftAsync();
		Task<FieldShift> GetFieldshiftByIdAsync(Guid id);
		Task<bool> CreateFieldshiftAsync(FieldShift fieldshift);
		Task<bool> UpdateFieldshiftAsync(Guid id, FieldShift updatedFieldShift);
		Task<bool> DeleteFieldshiftAsync(Guid id);
	}
}
