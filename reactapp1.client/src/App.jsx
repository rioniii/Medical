import React from 'react';
import Header from "./Header";
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import image1 from './assets/image1.jpg';
import image2 from './assets/image2.jpg';
import image3 from './assets/image3.jpg';
import image4 from './assets/image4.jpg';
import docPic from './assets/docPic.jpg';
import image6 from  './assets/image6.jpg';
function App() {
    const images = [image1, image2, image3, image4, docPic,image6];
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
