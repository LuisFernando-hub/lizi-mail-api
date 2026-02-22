namespace lizi_mail_api.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("api_keys")]
    public class ApiKeyEntity
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public UserEntity user { get; set; }
        public string name { get; set; }
        public string key_hash { get; set; }
        public bool is_active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime update_at { get; set; }

        public ApiKeyEntity()
        {

        }

        public ApiKeyEntity(string user_id)
        {
            this.id = Guid.NewGuid();
            this.user_id = Guid.Parse(user_id);
            this.name = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            this.key_hash = Guid.NewGuid().ToString();
            this.is_active = true;
            this.created_at = DateTime.UtcNow;
            this.update_at = DateTime.UtcNow;
        }
    }
}
