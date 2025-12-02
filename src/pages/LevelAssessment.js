// pages/LevelAssessment.js
import React, { useState } from 'react';
import Footer from '../components/Footer';
import { useCart } from '../context/CartContext';

const LevelAssessment = () => {
  const { addItem } = useCart();
  const [showResult, setShowResult] = useState(false);
  const [plan, setPlan] = useState(null);

  const handleSubmit = (e) => {
    e.preventDefault();
    
    const formData = new FormData(e.target);
    const sport = formData.get('sport');
    const experience = formData.get('experience');
    
    // Egyszerűsített generálás
    const generatedPlan = generateTrainingPlan(sport, experience);
    setPlan(generatedPlan);
    setShowResult(true);
  };

  const generateTrainingPlan = (sport, experience) => {
    // Egyszerűsített logika
    if (sport === 'football') {
      if (experience === 'beginner') {
        return {
          name: 'Labdarúgás fókuszú edzésterv - Kezdő',
          description: 'Ez a kezdő labdarúgó edzésterv az alapokra fókuszál.',
          duration: '8 hét',
          frequency: '3 alkalom/hét',
          suggestedProducts: [
            {
              id: 'product_focista_allokepesseg',
              name: 'Focista állóképesség',
              price: '12.990 Ft',
              category: 'Labdarúgás',
              image: 'https://images.unsplash.com/photo-1575361204480-aadea25e6e68?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
            }
          ]
        };
      }
    }
    // További esetek...
  };

  const handleAddSuggestedProduct = (product) => {
    addItem(product);
    alert(`${product.name} hozzáadva a kosárhoz!`);
  };

  return (
    <>
      <section className="hero-section" style={{background: 'linear-gradient(rgba(74, 144, 226, 0.85), rgba(44, 62, 80, 0.9))', padding: '80px 0'}}>
        <div className="container">
          <h1 className="hero-title">Ingyenes Szintfelmérő</h1>
          <p className="hero-subtitle">Fedezd fel a számodra legmegfelelőbb edzésprogramot!</p>
        </div>
      </section>

      <section className="py-5">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-lg-8">
              <div className="card shadow-sm border-0">
                <div className="card-body p-4">
                  <h2 className="text-center mb-4">Edzés szintfelmérő</h2>
                  <p className="text-center mb-4">Töltsd ki az alábbi kérdőívet, és mi ajánlunk számodra egy személyre szabott edzéstervet!</p>
                  
                  {!showResult ? (
                    <form id="assessmentForm" onSubmit={handleSubmit}>
                      {/* Űrlap mezők - leegyszerűsítve */}
                      <div className="mb-4">
                        <h4 className="mb-3">Alap információk</h4>
                        <div className="mb-3">
                          <label htmlFor="sport" className="form-label">Milyen sportág érdekel leginkább?</label>
                          <select className="form-select" id="sport" name="sport" required>
                            <option value="" selected disabled>Válassz...</option>
                            <option value="football">Labdarúgás</option>
                            <option value="fitness">Kondicionálás, fitness</option>
                          </select>
                        </div>
                        <div className="mb-3">
                          <label htmlFor="experience" className="form-label">Mennyi ideje edzel rendszeresen?</label>
                          <select className="form-select" id="experience" name="experience" required>
                            <option value="" selected disabled>Válassz...</option>
                            <option value="beginner">Kezdő (0-6 hónap)</option>
                            <option value="intermediate">Középhaladó (6 hónap - 2 év)</option>
                          </select>
                        </div>
                      </div>
                      
                      <div className="text-center">
                        <button type="submit" className="btn btn-primary btn-lg px-5">Edzésterv ajánlat kérése</button>
                      </div>
                    </form>
                  ) : (
                    <div id="result">
                      <div className="card border-success">
                        <div className="card-header bg-success text-white">
                          <h3 className="mb-0">Személyre szabott edzésterv ajánlat</h3>
                        </div>
                        <div className="card-body">
                          <h4 className="text-success mb-3">{plan.name}</h4>
                          <p className="mb-3">{plan.description}</p>
                          
                          <div className="row mb-4">
                            <div className="col-md-6">
                              <h5>Program részletei</h5>
                              <ul className="list-group list-group-flush">
                                <li className="list-group-item d-flex justify-content-between align-items-center">
                                  Időtartam
                                  <span className="badge bg-primary rounded-pill">{plan.duration}</span>
                                </li>
                                <li className="list-group-item d-flex justify-content-between align-items-center">
                                  Heti gyakoriság
                                  <span className="badge bg-primary rounded-pill">{plan.frequency}</span>
                                </li>
                              </ul>
                            </div>
                          </div>
                          
                          {plan.suggestedProducts && plan.suggestedProducts.length > 0 && (
                            <>
                              <h5 className="mt-5">Ajánlott edzéstervek</h5>
                              <div className="row">
                                {plan.suggestedProducts.map(product => (
                                  <div key={product.id} className="col-md-6 mb-3">
                                    <div className="card h-100">
                                      <div className="card-body d-flex flex-column">
                                        <h6 className="card-title">{product.name}</h6>
                                        <div className="d-flex justify-content-between align-items-center mt-auto">
                                          <span className="badge bg-primary">{product.category}</span>
                                          <span className="fw-bold text-primary">{product.price}</span>
                                        </div>
                                        <button 
                                          className="btn btn-primary btn-sm w-100 mt-2"
                                          onClick={() => handleAddSuggestedProduct(product)}
                                        >
                                          <i className="fas fa-cart-plus me-1"></i>Kosárba
                                        </button>
                                      </div>
                                    </div>
                                  </div>
                                ))}
                              </div>
                            </>
                          )}
                          
                          <div className="text-center mt-4">
                            <a href="/#training-plans" className="btn btn-primary">Fedezd fel edzésterveinket</a>
                          </div>
                        </div>
                      </div>
                    </div>
                  )}
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
      <Footer />
    </>
  );
};

export default LevelAssessment;