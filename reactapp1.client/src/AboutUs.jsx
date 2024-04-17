
import React from 'react';
//import { Container, Col } from 'react-bootstrap'; // Import Container and Col from react-bootstrap
import ChangeVideo from './video.module.css'; // Import the CSS file with the styles
import VideoIntro from '../src/assets/backroom.mp4';

function AboutUs  () {
    return (
        <>
           
          
                
            <div className={ChangeVideo.vid}>
                {/* Video element */}
                <video src={VideoIntro} type="video/mp4" className={ChangeVideo.bg} autoPlay muted loop preload="auto"></video>
                {/* Text content */}
                <div className={ChangeVideo.content}>
                    <h1>About Us</h1>
                    <h2>Welcome to Medical</h2>
                    <h3>Where we revolutionize hospital management with cutting-edge technology and a commitment to excellence.</h3>
                </div>
            </div>
            <div className={ChangeVideo.Kontekst}>
                        <h5>
                        
                           <div>
                            Our Mission<br></br>
                        At Meidal, our mission is simple: to streamline hospital operations
                        and improve patient care through innovative software solutions.
                          We believe that every hospital deserves access to advanced management tools that enhance efficiency, productivity, and overall performance.
                            </div>
                    <br></br>
            <br></br>

                            Why Choose Us?
                            Seamless Integration
                            Our management system seamlessly integrates into existing hospital infrastructure,<br/> minimizing disruption and maximizing effectiveness from day one.
                    <br></br>
                    <br></br>
                   
                            Customization
                            We understand that every hospital is unique. That's why our solutions are highly customizable, allowing you to tailor the system to meet the specific needs and workflows of your institution.
                    <br></br>
                    <br></br>
                            Real-time Insights
                            Gain invaluable insights into hospital operations with our real-time analytics dashboard. Monitor key performance metrics, identify trends, and make data-driven decisions to optimize efficiency and resource allocation.
                    <br></br>
                    <br></br>
                            Enhanced Patient Care
                            Our user-friendly interface empowers healthcare professionals to spend less time on administrative tasks and more time delivering quality care to patients. From appointment scheduling to electronic health records management, we simplify processes to prioritize what matters most patient well-being.
                    <br></br>
                    <br></br>
                            Security and Compliance
                            Protect patient data and ensure compliance with industry regulations with our robust security measures. Rest easy knowing that sensitive information is safeguarded against unauthorized access, breaches, and cyber threats.
                    <br></br>
                    <br></br>
                            Ongoing Support
                            Our commitment to your success doesn't end after implementation. Our dedicated support team is here to assist you every step of the way, providing ongoing training, troubleshooting, and updates to ensure optimal performance and satisfaction.
                    <br></br>
                    <br></br>
                            Let's Transform Healthcare Together
                            Join us in reshaping the future of healthcare management. Contact us today to learn more about our innovative solutions and how we can tailor them to meet the unique needs of your hospital.
                        </h5>
                   </div>
                
           
                
                
        </>
        );
   


}

export default AboutUs;


