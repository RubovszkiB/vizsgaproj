// components/FeaturesSection.js
import React from 'react';

const features = [
  {
    id: 1,
    icon: 'fas fa-user-check',
    title: 'Személyre szabott',
    description: 'Minden edzésterv egyedi igényekre szabható, céljaidnak megfelelően.'
  },
  {
    id: 2,
    icon: 'fas fa-chart-line',
    title: 'Folyamatos fejlődés',
    description: 'Kövesd fejlődésedet és kapj ajánlásokat a következő lépésekre.'
  },
  {
    id: 3,
    icon: 'fas fa-users',
    title: 'Közösség',
    description: 'Csatlakozz más sportolókhoz, ossz meg tapasztalatokat és motiváljátok egymást.'
  }
];

const FeaturesSection = () => {
  return (
    <section className="py-5">
      <div className="container">
        <div className="row text-center">
          {features.map(feature => (
            <div key={feature.id} className="col-md-4 mb-4">
              <div className="feature-icon mb-3">
                <i className={feature.icon}></i>
              </div>
              <h4>{feature.title}</h4>
              <p className="text-muted">{feature.description}</p>
            </div>
          ))}
        </div>
      </div>

      <style jsx>{`
        .feature-icon {
          font-size: 3rem;
          color: var(--primary-color);
          margin-bottom: 1.5rem;
        }
        
        .feature-icon i {
          transition: transform 0.3s ease;
        }
        
        .col-md-4:hover .feature-icon i {
          transform: scale(1.2);
        }
        
        h4 {
          font-weight: 600;
          margin-bottom: 1rem;
          color: var(--secondary-color);
        }
        
        p {
          line-height: 1.6;
          max-width: 300px;
          margin: 0 auto;
        }
      `}</style>
    </section>
  );
};

export default FeaturesSection;