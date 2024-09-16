using Codebase.Data;
using Codebase.Infrastructure.Game.Settings.Field;

namespace Codebase.Infrastructure.Abstract
{
    public interface IFieldDataSource
    {
        public FieldData[] GetFieldData(string name);
        string[] GetFieldsNames();
    }
}