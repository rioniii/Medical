import React from 'react';
import ChangeVideo from './video.module.css';
import VideoIntro from '../src/assets/backroom.mp4';
import Header from './Header';

function AboutUs() {
    return (
        <>
            <Header />
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
                <div className={ChangeVideo.content}>
                    <h1>About Us</h1>
                    <h2>Welcome to Medical</h2>
                    <h3>Where we revolutionize hospital management with cutting-edge technology and a commitment to excellence.</h3>
                </div>
            </div>

            <div className={ChangeVideo.kontekst}>
                <h5>Our Mission</h5>
                <p>
                    At Medical, our mission is simple: to streamline hospital operations
                    and improve patient care through innovative software solutions.
                    We believe that every hospital deserves access to advanced management tools that enhance efficiency, productivity, and overall performance.
                </p>

                <h5>Why Choose Us?</h5>
                <h6>Seamless Integration</h6>
                <p>
                    Our management system seamlessly integrates into existing hospital infrastructure,
                    minimizing disruption and maximizing effectiveness from day one.
                </p>

                <h6>Customization</h6>
                <p>
                    We understand that every hospital is unique. That's why our solutions are highly customizable,
                    allowing you to tailor the system to meet the specific needs and workflows of your institution.
                </p>

                <h6>Real-time Insights</h6>
                <p>
                    Gain invaluable insights into hospital operations with our real-time analytics dashboard.
                    Monitor key performance metrics, identify trends, and make data-driven decisions to optimize efficiency and resource allocation.
                </p>

                <h6>Enhanced Patient Care</h6>
                <p>
                    Our user-friendly interface empowers healthcare professionals to spend less time on administrative tasks
                    and more time delivering quality care to patients. From appointment scheduling to electronic health records management,
                    we simplify processes to prioritize what matters most: patient well-being.
                </p>

                <h6>Security and Compliance</h6>
                <p>
                    Protect patient data and ensure compliance with industry regulations with our robust security measures.
                    Rest easy knowing that sensitive information is safeguarded against unauthorized access, breaches, and cyber threats.
                </p>

                <h6>Ongoing Support</h6>
                <p>
                    Our commitment to your success doesn't end after implementation.
                    Our dedicated support team is here to assist you every step of the way, providing ongoing training, troubleshooting,
                    and updates to ensure optimal performance and satisfaction.
                </p>

                <h5>Let's Transform Healthcare Together</h5>
                <p>
                    Join us in reshaping the future of healthcare management.
                    Contact us today to learn more about our innovative solutions and how we can tailor them to meet the unique needs of your hospital.
                </p>
            </div>
        </>
    );
}

export default AboutUs;
