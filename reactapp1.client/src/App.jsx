import React from 'react';
import Header from "./Header";
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import image1 from './assets/Ballina.png';


function App() {
  
    const images = [image1];
    return (
        <div>
            <Header />
            <IntervalSlider images={images} interval={3000} />
            <Card />
            <Footer />
        </div>
    );
}


export default App;
