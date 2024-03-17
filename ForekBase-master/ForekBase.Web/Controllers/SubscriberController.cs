using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ForekBase.Web.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGetAllSubscribers()
        {
            IEnumerable<Subscriber> subscribers = _unitOfWork.Subscriber.GetAll(s => s.IsActive == true) ?? throw new ArgumentNullException(nameof(subscribers), "Subscribers not found");

            var subscribersVms = new List<SubscriberVM>();

            foreach (Subscriber subscriber in subscribers)
            {
                SubscriberVM subscriberVM = new()
                {
                    CreatedBy = subscriber.CreatedBy,
                    CreatedOn = subscriber.CreatedOn,
                    ModifiedBy = subscriber.ModifiedBy,
                    ModifiedOn = subscriber.ModifiedOn,
                    IsActive = subscriber.IsActive,
                    SubscriberID = subscriber.SubscriberID,
                    Category = subscriber.Category,
                    Email = subscriber.Email,
                    IsSubscribed = subscriber.IsSubscribed,

                };
                subscribersVms.Add(subscriberVM);
            }
            return View(subscribersVms);
        }

        public IActionResult OnGetSubscriber(Guid SubscriberId) 
        {
            Subscriber? subscriber = _unitOfWork.Subscriber.Get(u => u.SubscriberID == SubscriberId) ?? throw new ArgumentNullException(nameof(subscriber), "Post not found");

            SubscriberVM subscriberVM = new()
            {
                SubscriberID = subscriber.SubscriberID,
                CreatedOn = subscriber.CreatedOn,
                ModifiedOn = subscriber.ModifiedOn,
                CreatedBy = subscriber.CreatedBy,
                ModifiedBy = subscriber.ModifiedBy,
                IsActive = subscriber.IsActive,
                IsSubscribed = subscriber.IsSubscribed

            };

            if (ModelState.IsValid)
            {
                return View(subscriberVM);
            }

            return View();
        }

        public IActionResult OnCreateSubscriber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnCreateSubscriber(SubscriberVM subscriberVM)
        {
            if (subscriberVM is null)
            {
                throw new ArgumentNullException(nameof(subscriberVM), "Subscriber can not be null");
            }

            Subscriber? subscriber = new()
            {
                SubscriberID = Guid.NewGuid(),
                CreatedBy = subscriberVM.Email,
                CreatedOn = DateTime.Now,
                IsActive = true,
                Email = subscriberVM.Email,
                Category = subscriberVM.Category,
                IsSubscribed = true
            };

            if (subscriber != null)
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Subscriber.Add(subscriber);

                    _unitOfWork.Save();

                    TempData["success"] = "Saved successfully";

                    return RedirectToAction("Index", "Home");
                }

            }

            return View();

        }

        public IActionResult OnUpdateSubscriber(Guid SubscriberId) 
        {
            Subscriber? subscriber = _unitOfWork.Subscriber.Get(u => u.SubscriberID == SubscriberId) ?? throw new ArgumentNullException(nameof(subscriber), "Subscriber not found");

            SubscriberVM subscriberVM = new()
            {
                Category = subscriber.Category,
                IsActive = subscriber.IsActive,
                CreatedBy = subscriber.CreatedBy,
                CreatedOn = subscriber.CreatedOn,  
                Email = subscriber.Email,
                SubscriberID = subscriber.SubscriberID,
                ModifiedBy = subscriber.ModifiedBy,
                ModifiedOn = subscriber.ModifiedOn,
                IsSubscribed= subscriber.IsSubscribed,
                
            };

            return View(subscriberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnUpdatePost(SubscriberVM subscriberVM)
        {

            if (subscriberVM is null)
            {
                throw new ArgumentNullException(nameof(subscriberVM), "Subscriber can not be null");
            }

            Subscriber subscriber = new()
            {
                Category = subscriberVM.Category,
                IsActive = true,
                CreatedBy = subscriberVM.CreatedBy,
                CreatedOn = subscriberVM.CreatedOn,
                Email = subscriberVM.Email,
                SubscriberID = subscriberVM.SubscriberID,
                ModifiedBy = subscriberVM.ModifiedBy,
                ModifiedOn = subscriberVM.ModifiedOn,
                IsSubscribed = true,
            };

            if (ModelState.IsValid)
            {
                _unitOfWork.Subscriber.Update(subscriber);

                _unitOfWork.Save();

                TempData["success"] = "Saved successfully";

                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public IActionResult OnRemoveSubscriber(Guid SubscriberId)
        {
            var rmSubscriber = _unitOfWork.Subscriber.Get(s => s.SubscriberID == SubscriberId);

            if (rmSubscriber != null)
            {
                rmSubscriber.IsActive = false;

                _unitOfWork.Subscriber.Update(rmSubscriber);

                _unitOfWork.Save();

                TempData["success"] = "Subscriber removed successfully";

                return RedirectToAction("OnGetAllSubscribers", "Subscriber");
            }

            return View();
        }

        public IActionResult OnDisableSubscriber(Guid SubscriberId)
        {
            var subscriber = _unitOfWork.Subscriber.Get(p => p.SubscriberID == SubscriberId);

            if (subscriber != null)
            {
                subscriber.IsSubscribed = false;

                _unitOfWork.Subscriber.Update(subscriber);

                _unitOfWork.Save();

                TempData["success"] = "Subscriber disable successfully";

                return RedirectToAction("OnGetAllSubscribers", "Subscriber");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NotifySubscribers()
        {
            IEnumerable<Subscriber> subscribers = _unitOfWork.Subscriber.GetAll(s => s.IsActive == true && s.IsSubscribed == true) ?? throw new ArgumentNullException(nameof(subscribers), "Subscribers not found");

            foreach (Subscriber subscriber in subscribers)
            {
                OnSendSubscriberNotification(subscriber.Email);
            }

            return View("OnGetAllSubscribers", "Subscriber"); 
        }

        public static void OnSendSubscriberNotification(string to)
        {
            StringBuilder builder = new();

            var urlRef = "https://peopleseye.co.za/";

            builder.Append($"Hello People's eye Subscriber");

            builder.AppendLine();

            builder.Append($"Check out our latest stories on {urlRef}");

            builder.AppendLine();

            Application.Common.Utility.SD.OnSendMailNotification(to, "The People's eye", builder.ToString(), "The People's eye");

        }
    }
}
