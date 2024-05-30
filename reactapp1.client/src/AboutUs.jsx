
//import { Container, Col } from 'react-bootstrap'; // Import Container and Col from react-bootstrap

import React from 'react';

import VideoComponent from '../AboutUsComponentet/VideoComponent';
import MissionStatement from '../AboutUsComponentet/MissionStatement';
import Header from './Header';
import axiosInstance from './axiosConfig'; // axios cookie
function AboutUs  () {
    return (
        <>
           
            <Header /> 
                
            <VideoComponent />
            <MissionStatement />
                
           
                
                
        </>
        );
   


}

export default AboutUs;


