using CT.Core.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.Common.DTO
{
    public static partial class PersonAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="PersonDTO"/> converted from <see cref="Person"/>.</param>
        static partial void OnDTO(this Person entity, PersonDTO dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="Person"/> converted from <see cref="PersonDTO"/>.</param>
        static partial void OnEntity(this PersonDTO dto, Person entity);

        /// <summary>
        /// Converts this instance of <see cref="PersonDTO"/> to an instance of <see cref="Person"/>.
        /// </summary>
        /// <param name="dto"><see cref="ArtikalDTO"/> to convert.</param>
        public static Person ToEntity(this PersonDTO dto)
        {
            if (dto == null) return null;

            Person entity = null;

            entity = new Person();

            entity.IdPerson = dto.IdPerson;
            entity.Name = dto.Name;
            entity.SSN = dto.SSN;
            entity.DOB = dto.DOB;
            entity.CityHome = dto.CityHome;
            entity.StateHome = dto.StateHome;
            entity.StreetHome = dto.StreetHome;
            entity.ZipHome = dto.ZipHome;
            entity.CityOffice = dto.CityOffice;
            entity.StateOffice = dto.StateOffice;
            entity.StreetOffice = dto.StreetOffice;
            entity.ZipOffice = dto.ZipOffice;
            entity.Age = dto.Age;
            entity.Discount = dto.Discount;
            entity.FavoriteColors = dto.FavoriteColors;
            entity.UserId = dto.IdUser;
            entity.RecordTime = dto.RecordTime;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="Person"/> to an instance of <see cref="PersonDTO"/>.
        /// </summary>
        /// <param name="entity"><see cref="Person"/> to convert.</param>
        public static PersonDTO ToDTO(this Person entity)
        {
            if (entity == null) return null;

            PersonDTO dto = null;

            dto = new PersonDTO();

            dto.IdPerson = entity.IdPerson;
            dto.Name = entity.Name;
            dto.SSN = entity.SSN;
            dto.DOB = entity.DOB;
            dto.CityHome = entity.CityHome;
            dto.StateHome = entity.StateHome;
            dto.StreetHome = entity.StreetHome;
            dto.ZipHome = entity.ZipHome;
            dto.CityOffice = entity.CityOffice;
            dto.StateOffice = entity.StateOffice;
            dto.StreetOffice = entity.StreetOffice;
            dto.ZipOffice = entity.ZipOffice;
            dto.Age = entity.Age;
            dto.FavoriteColors = entity.FavoriteColors;
            dto.Discount = entity.Discount;
            dto.IdUser = entity.UserId;
            dto.RecordTime = entity.RecordTime;

            entity.OnDTO(dto);

            return dto;
        }

    }
}
