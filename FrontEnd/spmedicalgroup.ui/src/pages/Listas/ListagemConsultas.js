import React, { Component } from "react";
import { Link } from "react-router-dom";
import { logout } from "../../services/logout";

class ListagemConsultas extends Component {
    constructor() {
        super();
        this.state = {
            consultas: [],
           
        }     
        
    }


    // lista todas as consultas
    listarTodasConsultas() {
        fetch('http://localhost:5000/api/Consulta', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ consultas: data }))
            // .then(data => {
            //     if (data.status == 200) {
            //         this.setState({ consultas: data })
            //     }
            // })
            .catch(erro => console.log(erro))
    }


    // carrega o metodo
    componentDidMount() {
        this.listarTodasConsultas();
    }

 

    render() {
        return (
            <div>
                <table>
                    <tbody> 
                        <tr>
                            <th>Id</th>
                            <th>Id Prontuario</th>
                            <th>Nome Medico</th>
                            <th>Data</th>
                            <th>Status</th>
                            <th>Descricao</th>
                        </tr>

                        {
                            this.state.consultas.map(consulta => {
                                return (
                                    <tr key={consulta.id}>
                                        <td>{consulta.id}</td>
                                        <td>{consulta.idProntuario}</td>
                                        <td>{consulta.idMedicoNavigation.nome}</td>
                                        <td>{consulta.data.replace("T", " ").split(".")[0]}</td>
                                        <td>{consulta.idStatusNavigation.situacao}</td>
                                        <td>{consulta.descricao}</td>
                                    </tr>
                                );
                            } )
                        }
                    </tbody>
                </table>
                <Link onClick={logout}>Sair</Link>
            </div>
        );
    }
}


export default ListagemConsultas;