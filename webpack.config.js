const path = require('path');

module.exports = {
    devtool: false,
    mode: "development",
    target: 'web',
    entry: {
        app: './Client/app.ts',
        server: './Client/server.ts'
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
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
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
        extensions: ['.js', 'jsx', '.tsx', '.ts']
    }
};
