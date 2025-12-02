// pages/Home.js
import React from 'react';
import TrainingPlansCarousel from '../components/TrainingPlansCarousel';
import TrainersSection from '../components/TrainersSection';
import FeaturesSection from '../components/FeaturesSection';
import Footer from '../components/Footer';

const Home = () => {
  return (
    <>
      <section className="hero-section">
        <div className="container">
          <h1 className="hero-title">Éld át a sport varázsát!</h1>
          <p className="hero-subtitle">Személyre szabott edzéstervek profi edzőinktől</p>
          <a href="#training-plans" className="btn btn-primary btn-lg hero-cta">Fedezd fel edzéstervjeinket</a>
        </div>
      </section>

      <TrainingPlansCarousel />
      <TrainersSection />
      <FeaturesSection />
      <Footer />
    </>
  );
};

export default Home;