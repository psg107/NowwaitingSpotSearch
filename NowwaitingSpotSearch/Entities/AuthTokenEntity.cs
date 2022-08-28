using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace NowwaitingSpotSearch.Entities
{
    /// <summary>
    /// 토큰 정보
    /// </summary>
    [Table("TBL_AUTH_TOKEN")]
    [Index(nameof(State), IsUnique = true)]
    [Index(nameof(AccessToken), IsUnique = true)]
    public class AuthTokenEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 1)]
        public string AccessToken { get; set; }

        [Column(Order = 2)]
        public string Scope { get; set; }

        [Column(Order = 3)]
        public string TeamName { get; set; }

        [Column(Order = 4)]
        public string TeamId { get; set; }

        [Column(Order = 5)]
        public string? EnterpriseId { get; set; }

        [Column(Order = 6)]
        public string State { get; set; }
    }
}
