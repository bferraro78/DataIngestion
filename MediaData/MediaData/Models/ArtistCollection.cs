using System;

namespace MediaData.Models
{
    public class ArtistCollection : IEquatable<ArtistCollection>
    {
        public string ExportDate { get; set; }
        public string CollectionId { get; set; }
        public string ArtistId { get; set; }
        public bool IsPrimaryArtist { get; set; }
        public string RoleId { get; set; }

        public bool Equals(ArtistCollection other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return ArtistId.Equals(other.ArtistId);
        }

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashArtistId = ArtistId == null ? 0 : ArtistId.GetHashCode();

            //Calculate the hash code for the product.
            return hashArtistId;
        }
    }
}
