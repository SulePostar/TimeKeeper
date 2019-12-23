const path = require('path')
const htmlPlugin = require('html-webpack-plugin')

module.exports = {
    entry: path.join(__dirname, 'src'),
    output: {
        path: path.join(__dirname, 'dist'),
        publicPath: '/',
        filename: 'main.js'
    },
    devServer: {
        contentBase: path.join(__dirname, 'dist'),
        historyApiFallback: true,
        port: 3000
    },
    resolve: {
        alias: {
            Images: path.resolve(__dirname, './src/assets/images'),
            Styles: path.resolve(__dirname, './src/assets/styles'),
            Data: path.resolve(__dirname, './src/assets/data')
        }
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: 'babel-loader'
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader']
            },
            {
                test: /\.(jpg|png|svg|gif)$/,
                use: 'file-loader'
            }
        ]
    },
    plugins: [
        new htmlPlugin({
            title: 'Time React',
            favicon: 'src/favicon.png',
            template: 'src/index.html'
        })
    ]
}