import React from 'react'
import './Footer.css'
import img1 from './assets/FooterPic1.jpg'
import img2 from './assets/Footer2.jpg'
import img3 from './assets/Footer3.jpg'
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import './index.css';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
    faYoutube,
    faFacebook,
    faTwitter,
    faInstagram,
} from "@fortawesome/free-brands-svg-icons";


function Footer(){
    return (
        <footer>
            <div className="container">
                <div className="row">

                    <div className="col-lg-4 col-md-6">
                        <h3>Social Media</h3>
                        <ul className="list-unstyled three-column">
                            <a href="#" > <li>Home</li></a>
                            <a href="#" > <li>Services</li></a>
                            <a href="#" ><li>About</li>   </a>
                            <a href="#" ><li>Code</li>    </a>
                            <a href="#" ><li>Design</li>  </a>
                            <a href="#" ><li>Host</li>    </a>
                            <a href="#" ><li>Contact</li> </a>
                            <a href="#" ><li>Company</li> </a>
                        </ul>

                            
                                <a
                                    href="https://www.youtube.com/c/jamesqquick"
                                    className="youtube social"
                                >
                                    <FontAwesomeIcon icon={faYoutube} size="2x" />
                                </a>
                                <a
                                    href="https://www.facebook.com/learnbuildteach/"
                                    className="facebook social"
                                >
                                    <FontAwesomeIcon icon={faFacebook} size="2x" />
                                </a>
                                <a
                                    href="http://www.instagram.com/larnbuildteach"
                                    className="instagram social"
                                >
                                    <FontAwesomeIcon icon={faInstagram} size="2x" />
                                </a>
                                <a href="https://wwww.twitter.com" className="twitter social">
                                    <FontAwesomeIcon icon={faTwitter} size="2x" />
                                </a>

                    </div>

                    <div className="col-lg-4 col-md-6">
                        <h3>Contact</h3>
                        <div className="media">
                            <a href="#" className="pull-left">
                                <img src="  " width="64px" className="media-object" />
                            </a>
                            <div className="media-body">
                                <Form>
                                    <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
                                        <Form.Label>Subscribe here to get 10% discount</Form.Label>
                                        <Form.Control type="email" placeholder="Enter your email here..." />
                                        <Button className="subscribe" type="submit" variant="success">Subscribe</Button>
                                    </Form.Group>
                                </Form>
                            </div><br></br>
                        </div>

                        <div className="media">                     
                            <img src="https://static.vecteezy.com/system/resources/previews/006/541/488/original/clock-icon-isolated-on-a-white-background-symbol-of-time-for-your-website-design-illustration-free-vector.jpg"  className="media-object"  width="64px"/>
                           
                            <div className="media-body">
                                <h4 className="media-heading">Opening Hours</h4>
                                <p>Everyday: 24/7</p>
                            </div>
                        </div>

                        <div className="media">
                            <img src="https://cdn3.vectorstock.com/i/1000x1000/25/07/telephone-icon-phone-simple-or-logo-for-web-vector-11362507.jpg" width="64px"  className="media-object" />
                            <div className="media-body">
                                <h4 className="media-heading">Contact   </h4>
                                <p>Tel: 038 - 265- 928<br></br>   E-mail: info@medical-ks.com                         </p>
                            </div>
                        </div>

                    </div>

                    <div className="col-lg-4">
                        <h3>Our Work</h3>
                        <img className="img-thumbnail" src=" https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQAJiv8OLeod4V0Uu3iQnaBhq8nXFY6JoFfAq4fG7tzzQ&s" />
                        <img className="img-thumbnail" src={img2 } />
                        <img className="img-thumbnail" src={img3 } />
                    </div>

                </div>
            </div>
            <div className="copyright text-center">
                Copyright &copy; {new Date().getFullYear()} <span>Medical- All right reserved</span>
            </div>
        </footer>
    );

}
export default Footer;