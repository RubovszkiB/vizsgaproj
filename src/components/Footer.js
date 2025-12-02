// components/Footer.js
import React from 'react';

const Footer = () => {
  return (
    <footer className="py-4">
      <div className="container">
        <div className="row">
          <div className="col-md-6 mb-3 mb-md-0">
            <h5>Kapcsolat</h5>
            <p><i className="fas fa-envelope me-2"></i> info@rmbcoaching.hu</p>
            <p><i className="fas fa-phone me-2"></i> +36 1 234 5678</p>
            <p><i className="fas fa-map-marker-alt me-2"></i> 1061 Budapest, Andrássy út 1.</p>
          </div>
          <div className="col-md-6 text-md-end">
            <h5>Kövess minket</h5>
            <div className="social-icons">
              <a href="#"><i className="fab fa-facebook"></i></a>
              <a href="#"><i className="fab fa-instagram"></i></a>
              <a href="#"><i className="fab fa-youtube"></i></a>
              <a href="#"><i className="fab fa-tiktok"></i></a>
            </div>
            <p className="mt-3">&copy; 2023 RMB Coaching. Minden jog fenntartva.</p>
          </div>
        </div>
      </div>

      <style jsx>{`
        footer {
          background: var(--secondary-color);
          color: white;
        }
        
        h5 {
          font-weight: 600;
          margin-bottom: 1.5rem;
          color: white;
        }
        
        p {
          margin-bottom: 0.5rem;
        }
        
        .social-icons a {
          color: white;
          font-size: 1.25rem;
          margin-right: 1rem;
          transition: color 0.3s ease;
        }
        
        .social-icons a:hover {
          color: var(--primary-color);
        }
        
        i {
          width: 20px;
        }
      `}</style>
    </footer>
  );
};

export default Footer;