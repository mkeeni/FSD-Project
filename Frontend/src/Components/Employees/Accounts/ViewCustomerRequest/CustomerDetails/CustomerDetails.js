import '../../../../style.css';

function CustomerDetails(props){
    return(
        <div className='smallBox40 scrolling margin4'>
            <span className="clickRegisterText7">Name: {props.account.customers.name}</span>
            <span className="clickRegisterText7">Email: {props.account.customers.email}</span>
            <span className="clickRegisterText7">Date of Birth: {props.account.customers.dob}</span>
            <span className="clickRegisterText7">Phone Number: {props.account.customers.phoneNumber}</span>
            <span className="clickRegisterText7">Aadhaar Number: {props.account.customers.aadharNumber}</span>
            <span className="clickRegisterText7">PAN Number: {props.account.customers.panNumber}</span>
            <span className="clickRegisterText7">Address: {props.account.customers.address}</span>
            <span className="clickRegisterText7">Gender: {props.account.customers.gender}</span>
        </div>
    );
}

export default CustomerDetails;