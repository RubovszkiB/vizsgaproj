// pages/About.js
import React from 'react';
import TrainersSection from '../components/TrainersSection';
import Footer from '../components/Footer';

const About = () => {
  const values = [
    {
      id: 1,
      icon: 'fas fa-user-check',
      title: 'Személyre szabottság',
      description: 'Minden egyes edzéstervet és tanácsot a felhasználó egyedi igényei, céljai és képességei alapján alakítunk ki.'
    },
    {
      id: 2,
      icon: 'fas fa-graduation-cap',
      title: 'Szakmai hozzáértés',
      description: 'Edzőink kiváló szakmai háttérrel és gyakorlati tapasztalattal rendelkeznek különböző sportágakban.'
    },
    {
      id: 3,
      icon: 'fas fa-heart',
      title: 'Szenvedély',
      description: 'A sport iránt érzett szenvedélyünk vezérel minket, és ezt szeretnénk átadni minden egyes ügyfelünknek.'
    },
    {
      id: 4,
      icon: 'fas fa-chart-line',
      title: 'Folyamatos fejlődés',
      description: 'Mindig naprakészek maradunk a legújabb edzésmódszerekkel és tudományos kutatásokkal kapcsolatban.'
    },
    {
      id: 5,
      icon: 'fas fa-users',
      title: 'Közösségépítés',
      description: 'Olyan közösséget építünk, ahol az emberek motiválják és támogatják egymást az edzés során.'
    },
    {
      id: 6,
      icon: 'fas fa-shield-alt',
      title: 'Biztonság',
      description: 'Mindig a biztonságos és megfelelő technikákra helyezzük a hangsúlyt, hogy megelőzzük a sérüléseket.'
    }
  ];

  const timeline = [
    {
      id: 1,
      year: '2020',
      title: 'Alapítás',
      description: 'Három szenvedélyes sportoló, Bakaja Csaba, Rubovszki Balázs és Amesei Botond úgy döntött, hogy összehozza szakértelmét, és létrehozza az RMB Coaching-ot.'
    },
    {
      id: 2,
      year: '2021',
      title: 'Első sikerek',
      description: 'Az első évben már több mint 100 elégedett ügyfelünk volt, akik sikeresen érték el edzési céljaikat.'
    },
    {
      id: 3,
      year: '2022',
      title: 'Növekedés és bővülés',
      description: 'Megjelentünk a közösségi médiában, és jelentősen bővült ügyfélkörünk. Ebben az évben vezettük be a szintfelmérő rendszerünket.'
    },
    {
      id: 4,
      year: '2023',
      title: 'Új szint',
      description: 'Átléptük az 500 aktív felhasználó határt, és továbbfejlesztettük weboldalunkat, hogy még felhasználóbarátabb és funkcionálisabb legyen.'
    }
  ];

  const stats = [
    { id: 1, value: '500+', label: 'Elégedett ügyfél' },
    { id: 2, value: '50+', label: 'Egyedi edzésterv' },
    { id: 3, value: '3', label: 'Szakértő edző' },
    { id: 4, value: '98%', label: 'Elégedettségi arány' }
  ];

  return (
    <>
      <section className="hero-section" style={{
        background: 'linear-gradient(rgba(74, 144, 226, 0.9), rgba(44, 62, 80, 0.9)), url("https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80")',
        padding: '100px 0'
      }}>
        <div className="container">
          <h1 className="hero-title">Rólunk</h1>
          <p className="hero-subtitle">Ismerd meg a történetünket, küldetésünket és a mögöttünk álló szakértelmet</p>
        </div>
      </section>

      {/* Küldetés szekció */}
      <section className="py-5" style={{ background: 'linear-gradient(135deg, var(--primary-color) 0%, #6ab1f7 100%)', color: 'white' }}>
        <div className="container">
          <div className="row align-items-center">
            <div className="col-lg-6 mb-4 mb-lg-0">
              <h2 className="mb-4">Küldetésünk</h2>
              <p className="lead mb-4">Az RMB Coaching alapítói hiszünk abban, hogy a sport és a testmozgás nem csupán fizikai aktivitás, hanem életmód, amely pozitívan alakítja az emberek életét.</p>
              <p>Célunk, hogy mindenki megtalálja a számára megfelelő sportági és edzésformát, függetlenül korától, edzettségi szintjétől vagy korábbi tapasztalataitól.</p>
            </div>
            <div className="col-lg-6">
              <img 
                src="https://images.unsplash.com/photo-1571019614242-c5c5dee9f50b?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80" 
                alt="RMB Coaching csapat" 
                className="img-fluid rounded shadow" 
                style={{ maxHeight: '400px', objectFit: 'cover' }}
              />
            </div>
          </div>
        </div>
      </section>

      {/* Értékek szekció */}
      <section className="py-5" style={{ backgroundColor: 'var(--light-color)' }}>
        <div className="container">
          <h2 className="section-title text-center mb-5">Értékeink</h2>
          <div className="row">
            {values.map(value => (
              <div key={value.id} className="col-md-4 mb-4">
                <div className="value-card">
                  <div className="value-icon mb-3">
                    <i className={value.icon}></i>
                  </div>
                  <h4>{value.title}</h4>
                  <p className="text-muted">{value.description}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Történet szekció */}
      <section className="py-5">
        <div className="container">
          <h2 className="section-title text-center mb-5">Történetünk</h2>
          <div className="timeline">
            {timeline.map((item, index) => (
              <div key={item.id} className="timeline-item">
                <div className="timeline-content">
                  <h4>{item.year} - {item.title}</h4>
                  <p>{item.description}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Edzőink szekció */}
      <TrainersSection />

      {/* Statisztikák */}
      <section className="py-5" style={{
        background: 'linear-gradient(rgba(44, 62, 80, 0.9), rgba(44, 62, 80, 0.9)), url("https://images.unsplash.com/photo-1534438327276-14e5300c3a48?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80")',
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        color: 'white'
      }}>
        <div className="container">
          <h2 className="section-title text-center mb-5 text-white">Eredményeink számokban</h2>
          <div className="row text-center">
            {stats.map(stat => (
              <div key={stat.id} className="col-md-3 mb-4">
                <div className="stat-number" style={{ fontSize: '3rem', fontWeight: '700', color: 'var(--primary-color)' }}>
                  {stat.value}
                </div>
                <p>{stat.label}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* CTA */}
      <section className="py-5" style={{ backgroundColor: 'var(--primary-color)', color: 'white' }}>
        <div className="container text-center">
          <h2 className="mb-4">Készen állsz a változásra?</h2>
          <p className="lead mb-4">Csatlakozz több száz elégedett ügyfelünkhöz, és kezdd el sportos életútjadat még ma!</p>
          <div className="d-flex justify-content-center flex-wrap gap-3">
            <a href="/level-assessment" className="btn btn-light btn-lg">Ingyenes szintfelmérő</a>
            <a href="/#training-plans" className="btn btn-outline-light btn-lg">Fedezd fel edzésterveinket</a>
          </div>
        </div>
      </section>

      <Footer />

      <style jsx>{`
        .value-card {
          text-align: center;
          padding: 2rem;
          border-radius: 15px;
          transition: all 0.3s ease;
          height: 100%;
          background: white;
          border: none;
          box-shadow: 0 5px 15px rgba(0,0,0,0.08);
        }
        
        .value-card:hover {
          transform: translateY(-10px);
          box-shadow: 0 15px 30px rgba(0,0,0,0.15);
        }
        
        .value-icon {
          font-size: 3rem;
          color: var(--primary-color);
          margin-bottom: 1.5rem;
        }
        
        .timeline {
          position: relative;
          padding: 2rem 0;
        }
        
        .timeline::before {
          content: '';
          position: absolute;
          left: 50%;
          top: 0;
          bottom: 0;
          width: 4px;
          background: var(--primary-color);
          transform: translateX(-50%);
        }
        
        .timeline-item {
          margin-bottom: 3rem;
          position: relative;
        }
        
        .timeline-content {
          background: white;
          padding: 1.5rem;
          border-radius: 10px;
          box-shadow: 0 5px 15px rgba(0,0,0,0.08);
          position: relative;
          width: 45%;
        }
        
        .timeline-item:nth-child(odd) .timeline-content {
          left: 0;
        }
        
        .timeline-item:nth-child(even) .timeline-content {
          left: 55%;
        }
        
        .timeline-content::after {
          content: '';
          position: absolute;
          top: 20px;
          width: 20px;
          height: 20px;
          background: var(--primary-color);
          border-radius: 50%;
        }
        
        .timeline-item:nth-child(odd) .timeline-content::after {
          right: -10px;
          transform: translateX(50%);
        }
        
        .timeline-item:nth-child(even) .timeline-content::after {
          left: -10px;
          transform: translateX(-50%);
        }
        
        @media (max-width: 768px) {
          .timeline::before {
            left: 20px;
          }
          
          .timeline-content {
            width: calc(100% - 60px);
            left: 40px !important;
          }
          
          .timeline-content::after {
            left: -30px !important;
            transform: translateX(-50%);
          }
        }
      `}</style>
    </>
  );
};

export default About;