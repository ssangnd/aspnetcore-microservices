namespace Product.API.Extentions
{
    public static class ApplicationExtensions
    {
        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            //app.UseHttpsRedirection(); //for production only

            app.UseAuthorization();
            app.UseEndpoints(enpoints =>
            {
                enpoints.MapDefaultControllerRoute();
            });
        }
    }
}
