using System.Text.Json.Serialization;

namespace EFCoreRelationships
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string CharacterClass { get; set; } = "SwordUser";
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }
        public  Weapon Weapon { get; set; }
        public List<Skill> Skills { get; set; }
        
    }
}
