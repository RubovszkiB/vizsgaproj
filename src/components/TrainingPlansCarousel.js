// components/TrainingPlansCarousel.js
import React from 'react';
import { useCart } from '../context/CartContext';

const trainingPlans = [
  // Labdarúgás
  {
    id: 'product_focista_allokepesseg',
    name: 'Focista állóképesség',
    description: '12 hetes program a focisták számára, amely kiemelten foglalkozik a futási állóképesség fejlesztésével.',
    price: '12.990 Ft',
    category: 'Labdarúgás',
    image: 'https://images.unsplash.com/photo-1575361204480-aadea25e6e68?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80',
    badgeColor: 'bg-primary'
  },
  {
    id: 'product_labdaerintes_es_technika',
    name: 'Labdaérintés és technika',
    description: '8 hetes program, amely a labdakezelés, lövés és átadás technikájának tökéletesítésére fókuszál.',
    price: '9.990 Ft',
    category: 'Labdarúgás',
    image: 'https://images.unsplash.com/photo-1529900748604-07564a03e7a6?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80',
    badgeColor: 'bg-primary'
  },
  // További termékek...
];

const TrainingPlansCarousel = () => {
  const { addItem } = useCart();
  const [currentSlide, setCurrentSlide] = React.useState(0);
  const [newPlanIdea, setNewPlanIdea] = React.useState('');

  const handleAddToCart = (product) => {
    addItem(product);
  };

  const generateNewPlanIdea = () => {
    const ideas = [
      'Nyaralás előtti fitness program - 8 hetes kihívás a nyári formához!',
      'Otthoni HIIT program - Nincs szükség felszerelésre!',
      'Futók regenerációs program - Sebesség és állóképesség együtt!'
    ];
    setNewPlanIdea(ideas[Math.floor(Math.random() * ideas.length)]);
  };

  const slides = [
    trainingPlans.slice(0, 3),
    trainingPlans.slice(3, 6),
    trainingPlans.slice(6, 9)
  ];

  return (
    <section className="carousel-section" id="training-plans">
      <div className="container">
        <h2 className="section-title text-center mb-5">Népszerű edzésterveink</h2>
        
        <div id="trainingCarousel" className="carousel slide" data-bs-ride="carousel">
          <div className="carousel-inner">
            {slides.map((slide, index) => (
              <div 
                key={index} 
                className={`carousel-item ${index === currentSlide ? 'active' : ''}`}
              >
                <div className="row">
                  {slide.map(product => (
                    <div key={product.id} className="col-md-4 mb-4">
                      <div className="card training-card">
                        <img src={product.image} className="card-img-top" alt={product.name} />
                        <div className="card-body">
                          <h5 className="card-title">{product.name}</h5>
                          <p className="card-text">{product.description}</p>
                          <div className="d-flex justify-content-between align-items-center">
                            <span className={`badge ${product.badgeColor}`}>{product.category}</span>
                            <span className="fw-bold text-primary">{product.price}</span>
                          </div>
                        </div>
                        <div className="card-footer">
                          <button 
                            className="btn btn-primary w-100"
                            onClick={() => handleAddToCart(product)}
                          >
                            Kosárba
                          </button>
                        </div>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            ))}
          </div>
          
          <button 
            className="carousel-control-prev bg-dark rounded-circle" 
            onClick={() => setCurrentSlide(prev => prev === 0 ? slides.length - 1 : prev - 1)}
          >
            <span className="carousel-control-prev-icon" aria-hidden="true"></span>
            <span className="visually-hidden">Előző</span>
          </button>
          <button 
            className="carousel-control-next bg-dark rounded-circle"
            onClick={() => setCurrentSlide(prev => (prev + 1) % slides.length)}
          >
            <span className="carousel-control-next-icon" aria-hidden="true"></span>
            <span className="visually-hidden">Következő</span>
          </button>
        </div>
        
        <div className="text-center mt-5">
          <button 
            id="newPlanBtn" 
            className="btn btn-outline-primary btn-lg"
            onClick={generateNewPlanIdea}
          >
            <i className="fas fa-lightbulb me-2"></i>Új edzésterv ötlet
          </button>
          {newPlanIdea && (
            <div id="newPlanIdea" className="mt-3 alert alert-info" role="alert">
              {newPlanIdea}
            </div>
          )}
        </div>
      </div>
    </section>
  );
};

export default TrainingPlansCarousel;