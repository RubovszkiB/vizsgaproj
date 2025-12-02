// pages/Cart.js
import React from 'react';
import { useCart } from '../context/CartContext';
import Footer from '../components/Footer';
import CartItem from '../components/CartItem';

const Cart = () => {
  const { cartItems, getSubtotal, getTotal } = useCart();

  if (cartItems.length === 0) {
    return (
      <>
        <section className="cart-section">
          <div className="container">
            <h1 className="section-title mb-5">Bevásárlókosár</h1>
            <div className="empty-cart">
              <div className="empty-cart-icon">
                <i className="fas fa-shopping-cart"></i>
              </div>
              <h3>Az Ön kosara jelenleg üres</h3>
              <p className="text-muted mb-4">Még nem adott hozzá terméket a kosarához.</p>
              <a href="/#training-plans" className="btn btn-primary btn-lg">
                <i className="fas fa-arrow-left me-2"></i>Vissza a termékekhez
              </a>
            </div>
          </div>
        </section>
        <Footer />
      </>
    );
  }

  return (
    <>
      <section className="cart-section">
        <div className="container">
          <h1 className="section-title mb-5">Bevásárlókosár</h1>
          
          <div className="row">
            <div className="col-lg-8">
              <div id="cartItems">
                {cartItems.map(item => (
                  <CartItem key={item.id} item={item} />
                ))}
              </div>
            </div>
            <div className="col-lg-4">
              <div className="cart-summary">
                <h4 className="mb-4">Összegzés</h4>
                
                <div className="d-flex justify-content-between mb-3">
                  <span>Részösszeg:</span>
                  <span id="subtotal">{getSubtotal().toLocaleString()} Ft</span>
                </div>
                
                <div className="d-flex justify-content-between mb-3">
                  <span>Szállítás:</span>
                  <span id="shipping">Ingyenes</span>
                </div>
                
                <hr />
                
                <div className="d-flex justify-content-between mb-4">
                  <strong>Összesen:</strong>
                  <strong id="total">{getTotal().toLocaleString()} Ft</strong>
                </div>
                
                <button id="checkoutBtn" className="btn btn-primary w-100 btn-lg mb-3">
                  <i className="fas fa-credit-card me-2"></i>Tovább a fizetéshez
                </button>
                
                <a href="/#training-plans" className="btn btn-outline-primary w-100">
                  <i className="fas fa-plus me-2"></i>További termékek
                </a>
              </div>
            </div>
          </div>
        </div>
      </section>
      <Footer />
    </>
  );
};

export default Cart;