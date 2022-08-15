using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NowwaitingSpotSearch.Entities
{
    /// <summary>
    /// 가게 스팟 정보
    /// </summary>
    [Table("TBL_SPOT")]
    public class SpotEntity
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        /// <summary>
        /// 가게명
        /// </summary>
        [Required]
        [Column(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// 전화번호
        /// </summary>
        [Column(Order = 2)]
        public string? Phone { get; set; }

        /// <summary>
        /// 주소
        /// </summary>
        [Column(Order = 3)]
        public string? Address { get; set; }
    }
}
