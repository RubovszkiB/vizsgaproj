// components/Navbar.js
import React from 'react';
import { Link } from 'react-router-dom';
import { useCart } from '../context/CartContext';

const Navbar = () => {
  const { getTotalItems } = useCart();

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-white sticky-top shadow-sm">
      <div className="container">
        <Link className="navbar-brand" to="/">
          <span className="logo-text">RMB Coaching</span>
        </Link>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav me-auto">
            <li className="nav-item">
              <Link className="nav-link" to="/">Főoldal</Link>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/#training-plans">Edzéstervek</a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/#trainers">Edzőink</a>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/about">Rólunk</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/level-assessment">Ingyenes szintfelmérő</Link>
            </li>
          </ul>
          <ul className="navbar-nav">
            <li className="nav-item">
              <Link className="nav-link cart-icon" to="/cart">
                <i className="fas fa-shopping-cart"></i> 
                <span className="cart-count">{getTotalItems()}</span>
              </Link>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="#"><i className="fas fa-user"></i> Bejelentkezés</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;