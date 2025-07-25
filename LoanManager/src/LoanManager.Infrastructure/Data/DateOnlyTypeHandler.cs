// Importación de espacios de nombres necesarios
using System.Data; // Para trabajar con interfaces de datos y tipos de datos
using Dapper; // Para la biblioteca Dapper, un micro ORM para .NET

// Definición de la clase DateOnlyTypeHandler que hereda de SqlMapper.TypeHandler<DateOnly>
internal sealed class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    // Método que se encarga de convertir el valor de la base de datos a un tipo DateOnly
    public override DateOnly Parse(object value) 
    {
        // Convierte el valor de DateTime recibido en un objeto DateOnly
        return DateOnly.FromDateTime((DateTime)value);
    }

    // Método que establece el valor de un parámetro de la base de datos como DateOnly
    public override void SetValue(IDbDataParameter parameter, DateOnly value) 
    {
        // Establece el tipo de dato del parámetro a DbType.Date
        parameter.DbType = DbType.Date;
        
        // Asigna el valor del parámetro, que será el valor de DateOnly
        parameter.Value = value;
    }
}
