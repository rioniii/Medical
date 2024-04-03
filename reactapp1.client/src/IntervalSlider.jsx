import React, { useState, useEffect } from 'react';


const IntervalSlider = ({ images, interval = 3000 }) => {
    const [currentIndex, setCurrentIndex] = useState(0);

    useEffect(() => {
        const sliderInterval = setInterval(() => {
            setCurrentIndex((prevIndex) =>
                prevIndex === images.length - 1 ? 0 : prevIndex + 1
            );
        }, interval);

        return () => clearInterval(sliderInterval);
    }, [images.length, interval]);

    return (
        <div>
            <img src={images[currentIndex]} alt={`image ${currentIndex + 1}`} style={{ width: '100%' }} />        </div>
    );
};

export default IntervalSlider;
