using API_1.Reposetories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly ICatigoryRepo _catigoryRepo;

        public ProductController(IProductRepo productRepo, ICatigoryRepo catigoryRepo)
        {
            _productRepo = productRepo;
            _catigoryRepo = catigoryRepo;

        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productRepo.GetAll());
        }




        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Product product = _productRepo.GetById(id);

            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            return Ok(product);
        }


        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Product product = _productRepo.GetByName(name);

            if (product == null)
            {
                return NotFound(new { message = $"Product with Name {name} not found." });
            }

            return Ok(product);
        }


        #region ملحوظة مهمة اووووي
        /*
         
        
        سؤال --> طب انا مش فاهم برضه احنا بنبعت الاي دي في انانيموس اوبجكت ليه??ها لييييه 



        
        ماشي، خلينا نبسطها أكتر بحيث الفكرة توصل بسهولة.

ليه بنبعت الـ id جوه new { id = product.Id }؟
لما بنستخدم CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
اللي بيحصل هو:

CreatedAtAction بيرجع HTTP 201 Created، اللي معناه "تم إنشاء المورد بنجاح".
بيضيف Location Header في الاستجابة، اللي بيقول للـ client المكان اللي يقدر يلاقي فيه المنتج الجديد.
عشان ASP.NET Core يعرف يبني الـ URL صح، لازم نمرر القيم المطلوبة للـ route parameters في new { id = product.Id }.
مثال عملي:
تخيل إن عندك متابعة منتج معين بعد إنشائه، وعندك ميثود جلب منتج بالآي دي:

csharp
Copy
Edit
[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product == null) return NotFound();
    return Ok(product);
}
الميثود دي تستقبل id كـ route parameter، يعني لما العميل يطلب:

bash
Copy
Edit
GET /api/products/5
هتجيب المنتج رقم 5.

📌 طيب إزاي الـ POST يساعد هنا؟
لما تضيف منتج جديد:

csharp
Copy
Edit
[HttpPost]
public async Task<IActionResult> CreateProduct([FromBody] Product product)
{
    _context.Products.Add(product);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
}
nameof(GetProduct): بيحدد إنك عايز ترجع لينك للـ GetProduct.
new { id = product.Id }: بيوفر قيمة الـ id المطلوبة لبناء الرابط الصحيح.
🛠️ كيف يتم بناء رابط الـ Location؟
لو كان المنتج الجديد اتعمل بالـ id = 10، هيبعت السيرفر استجابة زي دي:

🔸 Response Headers
ruby
Copy
Edit
HTTP/1.1 201 Created
Location: https://yourapi.com/api/products/10
🔸 Response Body
json
Copy
Edit
{
  "id": 10,
  "name": "Sample Product",
  "price": 100.50,
  "categoryId": 1
}
🔹 هنا الـ Location Header اتكون بشكل ديناميكي، مش لازم نكتبه يدوي.
🔹 وده السبب اللي بيخلينا نمرر id جوه new { id = product.Id }.

💡 هل ممكن نشيل new { id = product.Id }؟
✅ نعم، لكن... الـ Location Header مش هيتكون تلقائيًا.
🔹 لو مش مهتم إنك ترجع رابط المورد الجديد، ممكن تعمل:

csharp
Copy
Edit
return StatusCode(201, product);
🔹 أو تحدد الرابط بنفسك (مش أفضل حل، لكنه ممكن):

csharp
Copy
Edit
return Created($"https://yourapi.com/api/products/{product.Id}", product);
🏁 الخلاصة
✔ new { id = product.Id } بيستخدم عشان ASP.NET Core يبني الرابط الديناميكي تلقائيًا.
✔ مش إجباري، لكن بدونها مش هيتم إنشاء الـ Location Header تلقائيًا.
✔ ممكن تستعمل StatusCode(201, product) لو مش محتاج الـ Location Header.

🚀 كده الصورة أوضح؟ 😃








You said:
طيب ليه برضه بعتناه في انانيموس اوبجكت مش رقم عادي
ChatGPT said:
سؤال ممتاز! 💡 ليه ما نبعتش product.Id كرقم عادي بدل ما نحطه في Anonymous Object (new { id = product.Id })؟

🔹 ليه استخدمنا new { id = product.Id } بدل product.Id مباشرة؟
السبب هو إن CreatedAtAction() يتوقع استلام بارامترات بنفس شكل الـ route parameters الموجودة في GetProduct API.

🚀 ASP.NET Core بيستخدم الـ Routing System علشان يولد الرابط تلقائيًا، ولأن الـ routes غالبًا بتحتوي على أكثر من بارامتر، لازم تبعت القيم دي في شكل object علشان يعرف يربط كل قيمة بالبارامتر الصحيح.

🔹 مثال عملي لتوضيح الفرق
✅ الطريقة الصحيحة (باستخدام Anonymous Object)
csharp
Copy
Edit
return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
new { id = product.Id } بيتم تفسيره بواسطة ASP.NET Core على إنه:
bash
Copy
Edit
GET /api/products/10
النظام بيقارن الـ { id } مع route parameters ({id} في [HttpGet("{id}")])، فيعرف يكوّن الرابط الصح.
❌ الطريقة الخاطئة (تمرير product.Id مباشرة)
csharp
Copy
Edit
return CreatedAtAction(nameof(GetProduct), product.Id, product);
📌 المشكلة هنا:

CreatedAtAction() ما بيقبلش رقم لوحده كـ route parameters.
هيرجع خطأ في التشغيل لأن النظام مش هيعرف يربط الرقم product.Id بالـ id في [HttpGet("{id}")].
حتى لو مشتغلتش بدون خطأ، مش هيتم تكوين الرابط بشكل صحيح.
🔹 طيب لو عندي أكثر من بارامتر في الـ Route؟
تخيل إن عندك API لازم تجيب المنتج حسب categoryId و productId، بالشكل ده:

csharp
Copy
Edit
[HttpGet("category/{categoryId}/product/{id}")]
public async Task<IActionResult> GetProduct(int categoryId, int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product == null) return NotFound();
    return Ok(product);
}
لو عايز ترجع CreatedAtAction() بشكل صحيح، هتحتاج تبعت جميع القيم المطلوبة في Anonymous Object:

csharp
Copy
Edit
return CreatedAtAction(nameof(GetProduct), new { categoryId = product.CategoryId, id = product.Id }, product);
🔹 كده ASP.NET Core يعرف يربط كل بارامتر بقيمته الصحيحة ويفهم إن {categoryId} بياخد product.CategoryId وإن {id} بياخد product.Id.

💡 الخلاصة
✔ CreatedAtAction() يحتاج أن تتطابق القيم مع أسماء البارامترات في الـ Route.
✔ لذلك نستخدم Anonymous Object (new { id = product.Id }) لتحديد القيم المرسلة بشكل صحيح.
✔ لو كان عندك أكثر من بارامتر في الـ Route، هتحتاج تبعت كل القيم المطلوبة بنفس الطريقة.

🔥 دلوقتي الصورة أوضح؟ 😃









         */
        #endregion


        [HttpPost]
        public IActionResult Create(Product product) 
        {

            _productRepo.CreateProduct(product);

            return CreatedAtAction("GetById" , new {id = product.Id } ,product);

           
        
        }



        [HttpPut]
        public IActionResult Update(Product product) 
        {

            _productRepo.Update(product);

            _productRepo.Save();


            return Ok("Updated succesufully");
        }


        [HttpGet("Nono")]
        IActionResult GetAllBew()
        {

            IEnumerable<Catigory> catigories = _catigoryRepo.GetAll();

            return Ok(catigories);


        }


    }
}
