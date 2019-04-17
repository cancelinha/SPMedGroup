import React from 'react';
import ReactDOM from 'react-dom';
import * as serviceWorker from './serviceWorker';
import {Route, BrowserRouter as Router, Switch, Redirect} from "react-router-dom";
import ConsultasPaciente from "./pages/consultas/ConsultasPaciente";
import ConsultasMedico from "./pages/consultas/ConsultasMedico";
import Login from "./pages/login/Login";

import {UsuarioAutenticado} from "./services/auth";
import {parseJwt} from "./services/auth";
import CadastroPaciente from './pages/cadastros/CadastroPaciente';

//VAI VERIFICAR SE E ADMINISTRADOR
const PermissaoAdmin = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().UsuarioTipo == "ADMINISTRADOR" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/"}}/>
            )
        }
    />
)

//ELE VAI VERIFICAR SE E MEDICO
const PermissaoMedico = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().UsuarioTipo == "MEDICO" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/"}}/>
            )
        }
    />
)

// ELE VAI VERIFICAR SE É PACIENTE
const PermissaoPaciente = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().UsuarioTipo == "PACIENTE" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/"}}/>
            )
        }
    />
)


const Routing = (
    <Router>
        <div>
            <Switch>
                <Route exact path="/" component={Login}/>
                <PermissaoPaciente path="/ConsultasPaciente" component={ConsultasPaciente} />
                <PermissaoMedico path="/ConsultasMedico" component={ConsultasMedico} />
                <Route path="/Cadastros" component={CadastroPaciente} />
                
                {/* <Route path="/DashBoard" component={DashBoard} />
                 */}
            </Switch>
        </div>
    </Router>
)
ReactDOM.render(Routing, document.getElementById('root'));

serviceWorker.unregister();
