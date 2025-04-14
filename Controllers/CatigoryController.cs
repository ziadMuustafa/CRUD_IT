using API_1.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatigoryController : ControllerBase
    {

        private readonly ICatigoryRepo _catigoryRepo;
        private readonly IProductRepo _productRepo;
        private readonly IGeneralResponse _generalResponse;

        public CatigoryController(ICatigoryRepo catigoryRepo, IProductRepo productRepo , IGeneralResponse generalResponse)
        {
            _catigoryRepo = catigoryRepo;
            _productRepo = productRepo;
            _generalResponse = generalResponse;
        }


        [HttpGet]
        public IActionResult GetAll()
        {

            List<Catigory> catigories = _catigoryRepo.GetAll().ToList();
             
            List<CatigoryWithProductDTO> CatDTO = new List<CatigoryWithProductDTO>();

            foreach (var item in catigories)
            {
                CatDTO.Add(new CatigoryWithProductDTO
                {
                    Id = item.Id,
                    CatigoryName = item.Name,
                    AllowedProducts = item.Products,
                    ProductsCount = item.Products.Count
                }); 

            }

            return Ok(CatDTO);

            
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Catigory catigory  = _catigoryRepo.GetById(id);

            if (catigory == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            CatigoryWithProductDTO catigoryWithProductDTO = new CatigoryWithProductDTO
            { Id = catigory.Id, CatigoryName = catigory.Name, AllowedProducts = catigory.Products  , ProductsCount = catigory.Products.Count};

            return Ok(catigoryWithProductDTO);
        }





        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Catigory catigory = _catigoryRepo.GetByName(name);

            if (catigory == null)
            {
                return NotFound(new { message = $"Product with Name {name} not found." });
            }

            CatigoryWithProductDTO catigoryWithProductDTO = new CatigoryWithProductDTO
            { Id = catigory.Id, CatigoryName = catigory.Name, AllowedProducts = catigory.Products  , ProductsCount = catigory.Products.Count};

            return Ok(catigoryWithProductDTO);
        }




        [HttpPost]
        public IActionResult Create(CatigoryWithProductDTO  catigoryWithProductDTO)
        {

            Catigory catigory = new Catigory { Name = catigoryWithProductDTO.CatigoryName };

            _catigoryRepo.Create(catigory);

            return CreatedAtAction("GetById", new { id = catigory.Id }, catigoryWithProductDTO);



        }

        #region ملاحظة مهمة جدااا عالابديت بال فيرب بوووت
        /*
         كان في مشكلة هنا اني كنت باخد ال الي دي من الروت يعني منتظر يجيلي من اليو ار ال كدة
         api/Catigory/id

         بس انا مكنتش مغير الروت عسان اخليه يستقبل الاي دي في اليو ار ال كدة 
         api/Catigory/id
        
         فكان لازم اغير الروت علشان اخليه يستقبل الاي دي في اليو ار ال كدة
         api/Catigory/{id} --- > api/Catigory/{id:int}
         
         ف كان لازم زي ما قولتلك اني احط هنا بليس هولدر عشان اقدؤ استقبل الاي دي 
         و يعمل ابديت مظبوط
         لانه كل مرة كان بيحاول يدور عالاي دي في الروت بس يلاقي انك مبعتش حاجة في الروت
         ف كان بيحطه بالديفولت دايما الي هو الصفر
         بالتالي كان بيحاول يعمل ابديت للكاتيجوري الي بالاي دي صفر
         بالتالي مكنش بيعمل ابديت للكاتيجوري الي انا عايزها
         وبالتالي مكنش بيلاقي الكاتيجوري الي انا عايزها فيقوم ضايف كل مرة كاتيجوري جديدة

         
         */
        #endregion

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id , CatigoryWithProductDTO  catigoryWithProductDTO)
        {
            if (id != 0 && _catigoryRepo.GetById(id) != null)
            {

                Catigory catigory = new Catigory { Name = catigoryWithProductDTO.CatigoryName, Id = id };

                _catigoryRepo.Update(catigory);

                _generalResponse.IsSuccess = true;
                _generalResponse.ResponseMessage = "Updated succesufully";

                return Ok(_generalResponse);
            }

            _generalResponse.IsSuccess = false;
            _generalResponse.ResponseMessage = "Not Updated , No valid catigory to update";
            return Ok(_generalResponse);
        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_catigoryRepo.GetById(id) != null && id != 0)
            {

                _catigoryRepo.Delete(id);
                _generalResponse.IsSuccess = true;
                _generalResponse.ResponseMessage = "Deleted succesufully";
                return Ok(_generalResponse);

            }

            _generalResponse.IsSuccess = false;
            _generalResponse.ResponseMessage = "Not Deleted , No valid catigory to delete";
            return Ok(_generalResponse);
        }


    }
}
