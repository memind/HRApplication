//using IKApplication.Domain.Entites;
//using Microsoft.AspNetCore.Mvc;

//namespace IKApplication.MVC.Controllers
//{
//    public class CompanyController : Controller
//    {
//        //private readonly ApplicationDbContext _context;
//        //public CompanyController(ApplicationDbContext context)
//        //{
//        //    _context = context;
//        //}

//        public IActionResult Index()
//        {
//            var companies = _context.Companies.ToList();
//            return View(companies);
//        }
//        public IActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Create(Company firm)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Firms.Add(firm);
//                _context.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(firm);
//        }
//}
