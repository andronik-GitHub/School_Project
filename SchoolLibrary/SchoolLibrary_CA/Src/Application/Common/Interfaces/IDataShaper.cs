﻿using Application.Common.HATEOS;

namespace Application.Common.Interfaces
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string? fieldsString);
        ShapedEntity ShapeData(T entity, string? fieldsString);
    }
}