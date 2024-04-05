import React from 'react';
import Header from "./Header";
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';
import i4 from './assets/i4.jpg';
function App() {
    const images = [i3, i2, i4];
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
