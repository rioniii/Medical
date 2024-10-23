import React from 'react';
import ChangeVideo from './video.module.css';
import VideoIntro from '../src/assets/backroom.mp4';
import Header from './Header';

function AboutUs() {
    return (
        <>
          
            <div className={ChangeVideo.kontekst}>
                {/* Video as background */}
                <div className={ChangeVideo.vid}>
                    <video
                        src={VideoIntro}
                        type="video/mp4"
                        className={ChangeVideo.bg}
                        autoPlay
                        muted
                        loop
                        preload="auto"
                    />
                </div>

                <div className={ChangeVideo.overlay}>
                    <h2 className={ChangeVideo.title}>About Us</h2>
                    <p className={ChangeVideo.description}>
                        At Medical, we strive to revolutionize hospital management through innovative software solutions. Our mission is to enhance operational efficiency and patient care, ensuring that every hospital has access to the tools necessary for success.
                    </p>

                    <h3 className={ChangeVideo.subtitle}>Our Vision</h3>
                    <p className={ChangeVideo.description}>
                        We envision a future where healthcare is streamlined and patient-centric, supported by cutting-edge technology that empowers healthcare providers.
                    </p>

                    <h3 className={ChangeVideo.subtitle}>What We Offer</h3>
                    <ul className={ChangeVideo.list}>
                        <li>Seamless integration into existing hospital systems.</li>
                        <li>Customizable solutions tailored to unique institutional needs.</li>
                        <li>Real-time analytics for informed decision-making.</li>
                        <li>User-friendly interfaces to enhance patient care.</li>
                        <li>Robust security measures for data protection.</li>
                        <li>Ongoing support and training from our dedicated team.</li>
                    </ul>

                    <h3 className={ChangeVideo.subtitle}>Join Us</h3>
                    <p className={ChangeVideo.description}>
                        Partner with us to reshape healthcare management. Contact us to learn more about how our innovative solutions can be tailored to meet the specific needs of your hospital.
                    </p>
                </div>
            </div>
        </>
    );
}

export default AboutUs;
