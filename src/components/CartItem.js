// components/CartItem.js
import React from 'react';
import { useCart } from '../context/CartContext';

const CartItem = ({ item }) => {
  const { removeItem, updateQuantity } = useCart();
  const price = parseInt(item.price.replace(/\D/g, ''));

  const handleQuantityChange = (newQuantity) => {
    updateQuantity(item.id, newQuantity);
  };

  const handleRemove = () => {
    removeItem(item.id);
  };

  return (
    <div className="cart-item">
      <div className="row align-items-center">
        <div className="col-md-2">
          <img src={item.image} alt={item.name} className="img-fluid rounded" />
        </div>
        <div className="col-md-5">
          <h5>{item.name}</h5>
          <span className="badge bg-primary">{item.category}</span>
        </div>
        <div className="col-md-3">
          <div className="quantity-controls">
            <button 
              className="quantity-btn"
              onClick={() => handleQuantityChange(item.quantity - 1)}
            >
              <i className="fas fa-minus"></i>
            </button>
            <input 
              type="text" 
              className="quantity-input" 
              value={item.quantity}
              readOnly
            />
            <button 
              className="quantity-btn"
              onClick={() => handleQuantityChange(item.quantity + 1)}
            >
              <i className="fas fa-plus"></i>
            </button>
          </div>
        </div>
        <div className="col-md-2 text-end">
          <div className="fw-bold text-primary">
            {(price * item.quantity).toLocaleString()} Ft
          </div>
          <button 
            className="remove-btn mt-2"
            onClick={handleRemove}
          >
            <i className="fas fa-trash"></i> Eltávolítás
          </button>
        </div>
      </div>
    </div>
  );
};

export default CartItem;