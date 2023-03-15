using Card.Api.Database;
using Card.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Card.Api.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly CardDbContext cardDbContext;
        public CardController(CardDbContext cardDbContext)
        {
            this.cardDbContext = cardDbContext;
        }

        //Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await cardDbContext.Cards.ToListAsync();
            return Ok(cards);
        }

        //Get Single Card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await cardDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("Card Not Found");


        }
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Cards card)
        {
            card.Id = Guid.NewGuid();
            await cardDbContext.Cards.AddAsync(card);
            await cardDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(AddCard), new {id = card.Id}, card);
        }

        //Updaing A Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatingCard([FromRoute] Guid id, [FromBody] Cards card)
        {
            var OldCard = await cardDbContext.Cards.FirstOrDefaultAsync(card => card.Id == id);
            if (OldCard != null)
            {
                OldCard.CardName = card.CardName;
                OldCard.CardNumber = card.CardNumber;
                OldCard.ExpiryMonth = card.ExpiryMonth;
                OldCard.ExpiryYear = card.ExpiryYear;
                OldCard.CVC = card.CVC;
                await cardDbContext.SaveChangesAsync();
                return Ok(OldCard);
            }
            return NotFound("Not Found");

        }


        //Delete A Card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DelteCard([FromRoute] Guid id)
        {
            var OldCard = await cardDbContext.Cards.FirstOrDefaultAsync(card => card.Id == id);
            if (OldCard != null)
            {
                cardDbContext.Remove(OldCard);
                await cardDbContext.SaveChangesAsync();
                return Ok(OldCard);
            }
            return NotFound("Not Found");

        }
    }
}
