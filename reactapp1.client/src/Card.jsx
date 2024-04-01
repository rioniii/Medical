import examplePicture from './assets/Dr.-Shpetim-Thaci.jpg';
function Card() {


    return (
        <div className="card">
            <img className="card-img" src={examplePicture} alt="profile picture"></img>
            <h2> Dr.Shp&euml;tim Thaqi</h2>
            <p>MD. PhD Specialist i Pulmologjise</p>
        </div>   
 
    );
}

export default Card;