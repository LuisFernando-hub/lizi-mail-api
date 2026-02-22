using lizi_mail_api.Entities.Enuns;
using static System.Net.WebRequestMethods;

namespace lizi_mail_api.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("emails")]
    public class EmailEntity
    {
        public Guid id { get; set; }

        public Guid user_id { get; set; }
        public UserEntity user { get; set; }
        public Guid api_key_id { get; set; }
        public ApiKeyEntity api_key { get; set; }
        public string to_email { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string provider { get; set; }
        public string provider_message_id { get; set; }
        public string error_message { get; set; }

        public StatusEmail status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime sent_at { get; set; }
    }
}
