
using lizi_mail_api.Entities.Enuns;

namespace lizi_mail_api.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("users")]
    public class UserEntity
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public PlanType plan { get; set; }
        public bool is_active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime update_at { get; set; }

        public UserEntity(string name, string email, string password_hash)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.email = email;
            this.password_hash = password_hash;
            this.plan = PlanType.free;
            this.is_active = true;
            this.created_at = DateTime.UtcNow;
            this.update_at = DateTime.UtcNow;
        }
    }
}
