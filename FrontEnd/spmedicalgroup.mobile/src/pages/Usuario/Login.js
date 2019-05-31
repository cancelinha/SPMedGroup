import React, {Component} from 'react';
import { View, Text, TextInput, TouchableOpacity,StyleSheet, Image, AsyncStorage} from 'react-native';
import api from '../../services/api';


class Login extends Component{
    static navigationOptions={
        tabBarIcon: ({tintcolor}) =>{
            <Image
            source={require('../../assets/img/BackgroundMed.jpg')}
            style={StyleSheet.BackNavigator}
            />
        }
    }

    constructor(props){
        super(props);
        this.state ={
            email:"",
            senha:""
        }
    }

    login = async () => {
        console.warn("Log ok 1")
        const resposta = await api.post("/Login", {
            email: this.state.email,
            senha: this.state.senha
          });
          console.warn("Log ok 2")
          const token = resposta.data.token;
          console.warn("Log ok 3")
          await AsyncStorage.setItem("userToken", token);
          console.warn("Log ok 4")
          this.props.navigation.navigate("MainNavigator");
          console.warn("log ok 5")
      };

      render(){
        return(
            <View>
                <View style={styles.cabecalho}>
                    <Text style={styles.titulo}>Login</Text>
                    <View style={styles.linhaTitulo} />
                </View>
                <TextInput style = {styles.input} 
                    autoCapitalize="none"
                    keyboardType='email-address' 
                    returnKeyType="next" 
                    placeholder='Email' 
                    onChangeText={email => this.setState({ email })}
                    placeholderTextColor='rgb(161, 162, 163)'/>

                <TextInput style = {styles.input}
                    placeholder='Password' 
                    onChangeText={senha => this.setState({ senha })}
                    placeholderTextColor='rgb(161, 162, 163)' 
                    secureTextEntry/>

                <TouchableOpacity style={styles.buttonContainer}
                onPress={this.login}
                >
                            <Text  style={styles.buttonText}>LOGIN</Text>
                </TouchableOpacity>     
            </View>
        );
    }

}
const styles = StyleSheet.create({
    BackNavigator:{
        width: 25,
        height:25, 
        tintColor: "white"
    },
    container: {
        padding: 20
       },
       input:{
           height: 40,
           backgroundColor: 'rgba(225,225,225,0.2)',
           marginBottom: 10,
           padding: 10,
           color: 'red',
           width: 376,
           marginLeft: 16
       },
       buttonContainer:{
           backgroundColor: '#2980b6',
           paddingVertical: 15,
           width: 300,
           marginLeft: 55
       },
       buttonText:{
           color: '#fff',
           textAlign: 'center',
           fontWeight: '700'
       },
       titulo: {
        fontSize: 16,
        letterSpacing: 1
    },
    cabecalho: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
        marginTop: 30,
        marginBottom:30
    },

      linhaTitulo:{
        width: 100,
        borderBottomColor: "#999999",
        borderBottomWidth: 0.9,
        marginBottom: 8,
        marginTop: 2
    }
});

export default Login;
