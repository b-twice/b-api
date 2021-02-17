
.PHONEY: entities

#see this for sed reason: https://github.com/dotnet/efcore/issues/119610
entities:
	cd b.Entities; \
	dotnet ef dbcontext scaffold "DataSource=${HOME}/data/app.db;" Microsoft.EntityFrameworkCore.Sqlite \
	-v -c AppDbContext --force --no-onconfiguring; \
	sed -i '' 's/ValueGeneratedNever/ValueGeneratedOnAdd/g' AppDbContext.cs