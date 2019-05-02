const path = require('path');

module.exports = [{
    mode: "development",
    target: 'web',
    entry: {
        app: './Client/App.js'
    },
    devtool: "source-map",
    output: {
        path: path.join(__dirname, 'wwwroot', 'js'),
        filename: '[name].js',
        chunkFilename: 'app.[name].[hash].js',
        publicPath: '/js/'
    },
    module: {
        rules: [
            {
                rules: [
                  {
                    test: /\.(js|jsx)$/,
                    exclude: /node_modules/,
                    loader: "babel-loader",
                    options: {
                        presets: ["@babel/preset-env"]
                    }
                  }
                ]
              },
            {
                test: /App.js/,
                use: [{
                    loader: 'expose-loader',
                    options: 'Components'
                }]
            },
            {
                test: require.resolve('react-dom'),
                use: [{
                    loader: 'expose-loader',
                    options: 'ReactDOM'
                }]
            },
            {
                test: require.resolve('react'),
                use: [{
                    loader: 'expose-loader',
                    options: 'React'
                }]
            }
        ]
    },
    resolve: {
        modules: [path.resolve(__dirname, 'Client'), 'node_modules'],
        extensions: ['.ts', '.jsx', '.js']
    }
}];
