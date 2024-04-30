import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { updateCustomerID } from "../../../../customerSlice";

function Customer(props){

    var dispatch = useDispatch();
    var navigate = useNavigate();

    var email = sessionStorage.getItem('email');

    function updateCustomerId(){
        if(email.includes("@maverick.in")){
            navigate("/employeeMenu/viewCustomerDetails");
            dispatch(
                updateCustomerID(props.customer.customerID)
            );
        }
        else{
            navigate("/adminMenu/viewCustomer");
            dispatch(
                updateCustomerID(props.customer.customerID)
            );
        }
    }

    return (
        <div className="whiteOutlineBox7">
            <div className="whiteOutlineBoxMargin">
                <span className="clickRegisterText">Name: {props.customer.name}</span>
                <div className="smallBox23">
                    <span className="clickRegisterText">Email: {props.customer.email}</span>
                    <span className="pointer" onClick={updateCustomerId}>
                        <div className="rightArrow2 change-my-color"></div>
                    </span>
                </div>
            </div>
        </div>
    ); 
}

export default Customer;