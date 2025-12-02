// components/TrainersSection.js
import React from 'react';

const trainers = [
  {
    id: 1,
    name: 'Bakaja Csaba',
    specialty: 'Labdarúgás szakértő',
    description: '10+ év profi labdarúgó tapasztalat. UEFA B licenccel rendelkezik. Fiatal tehetségek felkészítésében szakosodott.',
    image: 'https://images.unsplash.com/photo-1560272564-c83b66b1ad12?ixlib=rb-4.0.3&auto=format&fit=crop&w=634&q=80',
    stats: {
      trained: '250+',
      experience: '8'
    }
  },
  {
    id: 2,
    name: 'Rubovszki Balázs',
    specialty: 'Kondicionáló edző',
    description: 'Testépítési versenyek győztese, személyi edzői oklevéllel rendelkezik. Testösszetétel optimalizálásában szakosodott.',
    image: 'https://images.unsplash.com/photo-1581009146145-b5ef050c2e1e?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80',
    stats: {
      trained: '500+',
      experience: '12'
    }
  },
  {
    id: 3,
    name: 'Mezei Botond',
    specialty: 'Mixed szakértő',
    description: 'Több sportágban is versenyszerűen aktív. Football felkészítés nagy tornákra, edzőtermi edzésekkel egyaránt.',
    image: 'https://img.hvg.hu/Img/8133bb77-3fc3-490f-b374-cb198a0455cc/54defe14-8d21-4f01-a6ed-ef782b12039c.jpg',
    stats: {
      trained: '300+',
      experience: '11'
    }
  }
];

const TrainersSection = () => {
  return (
    <section className="trainers-section py-5 bg-light" id="trainers">
      <div className="container">
        <h2 className="section-title text-center mb-5">Edzőink</h2>
        
        <div className="row">
          {trainers.map(trainer => (
            <div key={trainer.id} className="col-md-4 mb-4">
              <div className="card trainer-card text-center">
                <div className="trainer-img-container">
                  <img src={trainer.image} className="card-img-top trainer-img" alt={trainer.name} />
                  <div className="trainer-overlay">
                    <div className="trainer-social">
                      <a href="#"><i className="fab fa-facebook"></i></a>
                      <a href="#"><i className="fab fa-instagram"></i></a>
                      <a href="#"><i className="fab fa-twitter"></i></a>
                    </div>
                  </div>
                </div>
                <div className="card-body">
                  <h5 className="card-title">{trainer.name}</h5>
                  <p className="card-text trainer-specialty">{trainer.specialty}</p>
                  <p className="card-text">{trainer.description}</p>
                  <div className="trainer-stats">
                    <div className="stat-item">
                      <span className="stat-number">{trainer.stats.trained}</span>
                      <span className="stat-label">Képzett sportoló</span>
                    </div>
                    <div className="stat-item">
                      <span className="stat-number">{trainer.stats.experience}</span>
                      <span className="stat-label">Év tapasztalat</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>

      <style jsx>{`
        .trainer-img-container {
          position: relative;
          overflow: hidden;
          height: 250px;
        }
        
        .trainer-img {
          height: 100%;
          width: 100%;
          object-fit: cover;
        }
        
        .trainer-overlay {
          position: absolute;
          top: 0;
          left: 0;
          right: 0;
          bottom: 0;
          background: rgba(0, 0, 0, 0.5);
          opacity: 0;
          transition: opacity 0.3s ease;
          display: flex;
          align-items: center;
          justify-content: center;
        }
        
        .trainer-img-container:hover .trainer-overlay {
          opacity: 1;
        }
        
        .trainer-social a {
          color: white;
          font-size: 1.5rem;
          margin: 0 0.5rem;
          transition: color 0.3s ease;
        }
        
        .trainer-social a:hover {
          color: var(--primary-color);
        }
        
        .trainer-card {
          border: none;
          border-radius: 15px;
          overflow: hidden;
          transition: transform 0.3s ease, box-shadow 0.3s ease;
        }
        
        .trainer-card:hover {
          transform: translateY(-5px);
          box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
        }
        
        .trainer-specialty {
          color: var(--primary-color);
          font-weight: 600;
          margin-bottom: 1rem;
        }
        
        .trainer-stats {
          display: flex;
          justify-content: center;
          gap: 2rem;
          margin-top: 1.5rem;
          padding-top: 1.5rem;
          border-top: 1px solid #e9ecef;
        }
        
        .stat-item {
          display: flex;
          flex-direction: column;
          align-items: center;
        }
        
        .stat-number {
          font-size: 1.5rem;
          font-weight: 700;
          color: var(--primary-color);
        }
        
        .stat-label {
          font-size: 0.875rem;
          color: #6c757d;
        }
      `}</style>
    </section>
  );
};

export default TrainersSection;