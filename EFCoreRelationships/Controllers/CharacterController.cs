

using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CharacterController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {
            var characters = await _dataContext.Characters
                .Where(x => x.UserId == userId)
                .Include(c=> c.Weapon)
                .ToListAsync();
            return characters;
        }
        [HttpPost]
        public async Task<ActionResult<List<Character>>> Create(CreateCharacterDTO createCharacter)
        {
            var user = await _dataContext.Users.FindAsync(createCharacter.UserId);
            if (user == null)
            {
                return NotFound();
            }
            var newCharacter = new Character
            {
                Name = createCharacter.Name,
                CharacterClass = createCharacter.CharacterClass,
                User = user
            };
             _dataContext.Characters.Add(newCharacter);
            await _dataContext.SaveChangesAsync();
            return await Get(newCharacter.UserId);
        }
        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> AddWeapon(AddWeaponDTO addWeapon)
        {
            var character = await _dataContext.Characters.FindAsync(addWeapon.CharacterId);
            if (character == null)
            {
                return NotFound();
            }
            var newWeapon = new Weapon
            {
                Name = addWeapon.Name,
                Damage = addWeapon.Damage,
                Character = character  
            };
            _dataContext.Weapons.Add(newWeapon);
            await _dataContext.SaveChangesAsync();

            return character;
        }
    }
    
}
