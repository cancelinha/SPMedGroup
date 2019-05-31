import { createBottomTabNavigator, createAppContainer, createStackNavigator, createSwitchNavigator } from 'react-navigation';
import Login from './pages/Usuario/Login'

const AuthStack = createStackNavigator({ Login   });

const PrincipalRoute = createBottomTabNavigator(
    {
        Login
    }
);

const First = createAppContainer(PrincipalRoute);

export default First;