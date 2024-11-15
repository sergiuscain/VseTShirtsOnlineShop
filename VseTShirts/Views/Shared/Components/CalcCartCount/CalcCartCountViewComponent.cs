﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;

namespace VseTShirts.Views.Shared.Components.CalcCartCount
{
    public class CalcCartCountViewComponent : ViewComponent
    {
        private readonly ICartsStorage _cartStorage;
        private readonly UserManager<User> userManager;
        public CalcCartCountViewComponent(ICartsStorage cartStorage, UserManager<User> userManager)
        {
            _cartStorage = cartStorage;
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var cart = await _cartStorage.GetCartByUserIdAsync(user.Id);//.Positions.Count();
                var positions = cart == null ? null : cart.Positions;
                int count = positions == null ? 0 : Helper.ToViewModel(cart).productsCountInCart;
                return View("CalcCartCountViewComponent", count);
            }
            catch
            {
                return View("CalcCartCountViewComponent", 0);
            }
        }
    }
}
